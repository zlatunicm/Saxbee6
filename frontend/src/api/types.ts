export type HiveType = 'Langstroth' | 'Dadant' | 'Farrar' | 'Warré' | 'TopBar' | 'Other';
export type HiveStatus = 'Active' | 'Inactive' | 'Dead' | 'Swarmed';
export type QueenStatus = 'Present' | 'Missing' | 'Laying' | 'NotLaying' | 'Superseded' | 'Replaced';
export type BroodPattern = 'Excellent' | 'Good' | 'Fair' | 'Poor';
export type ColonyStrength = 'Strong' | 'Medium' | 'Weak' | 'VeryWeak';
export type Temperament = 'Calm' | 'Gentle' | 'Normal' | 'Defensive' | 'Aggressive';
export type TreatmentType = 'Varroa' | 'Nosema' | 'AmericanFoulbrood' | 'EuropeanFoulbrood' | 'Chalkbrood' | 'Pesticide' | 'Other';
export type HarvestType = 'Honey' | 'Beeswax' | 'Propolis' | 'RoyalJelly' | 'Pollen';
export type FeedType = 'SugarSyrup' | 'DrysugarCake' | 'Pollen' | 'Fondant' | 'Other';
export type TaskPriority = 'Low' | 'Medium' | 'High';

export interface Apiary {
  id: number; name: string; description?: string; location?: string;
  latitude?: number; longitude?: number; createdAt: string; hiveCount: number;
}

export interface Queen {
  id: number; name?: string; yearBorn?: number; status: QueenStatus;
  origin?: string; markingColor?: string; isMarked: boolean; isClipped: boolean;
  notes?: string; hiveId: number;
}

export interface HiveSummary {
  id: number; name: string; type: HiveType; status: HiveStatus;
  color?: string; apiaryId: number; queenStatus?: QueenStatus; lastInspection?: string;
}

export interface Hive {
  id: number; name: string; description?: string; type: HiveType; status: HiveStatus;
  establishedDate: string; color?: string; notes?: string;
  apiaryId: number; apiaryName: string; queen?: Queen;
}

export interface Inspection {
  id: number; inspectedAt: string; strength: ColonyStrength; broodPattern: BroodPattern;
  temperament: Temperament; queenSeen: boolean; eggsSeen: boolean;
  queenCellsPresent: boolean; swarmingSign: boolean;
  honeyStoresFrames?: number; broodFrames?: number; beesFrames?: number;
  varroaCount?: number; notes?: string; weather?: string;
  temperatureCelsius?: number; hiveId: number; hiveName: string;
}

export interface Treatment {
  id: number; type: TreatmentType; product: string; dosage?: string;
  startDate: string; endDate?: string; notes?: string; hiveId: number; hiveName: string;
}

export interface Harvest {
  id: number; type: HarvestType; quantityKg: number; harvestedAt: string;
  notes?: string; hiveId: number; hiveName: string;
}

export interface Feeding {
  id: number; feedType: FeedType; quantityKg: number; fedAt: string;
  notes?: string; hiveId: number; hiveName: string;
}

export interface BeeTask {
  id: number; title: string; description?: string; priority: TaskPriority;
  dueDate?: string; isCompleted: boolean; createdAt: string;
  hiveId?: number; apiaryId?: number;
}
